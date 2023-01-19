// SPDX-License-Identifier: MIT

pragma solidity 0.7.6;
pragma abicoder v2;

import "@openzeppelin/contracts-upgradeable/math/SignedSafeMathUpgradeable.sol";
import "@openzeppelin/contracts-upgradeable/math/SafeMathUpgradeable.sol";
import "./LibIntMapping.sol";


library safeU64 {
    function set(uint base, uint offset, uint data) internal pure returns(uint) {
        uint old = uint(get(base, offset));
        uint result = base - (old << (64 * (offset)));
        return result + (data << (64 * (offset)));
    }

    function get(uint data, uint offset) internal pure returns(uint){
        return uint(data >> (64 * offset));
    }

    function start(uint data) internal pure returns(uint){
        return uint(data >> (64 * 0));
    }

    function bias(uint data) internal pure returns(uint){
        return uint(data >> (64 * 1));
    }

    function slope(uint data) internal pure returns(uint){
        return uint(data >> (64 * 2));
    }

    function cliff(uint data) internal pure returns(uint){
        return uint(data >> (64 * 3));
    }

    function setStart(uint base, uint data) internal pure returns(uint){
        return set(base, 0, data);
    }

    function setBias(uint base, uint data) internal pure returns(uint){
        return set(base, 1, data);
    }

    function setSlope(uint base, uint data) internal pure returns(uint){
        return set(base, 2, data);
    }

    function setCliff(uint base, uint data) internal pure returns(uint){
        return set(base, 3, data);
    }

    function create(uint _start, uint _bias, uint _slope, uint _cliff) internal pure returns(uint){
        uint result;
        if (_start > 0) {
            result = setStart(result, _start);
        }
        if (_bias > 0) {
            result = setBias(result, _bias);
        }
        if (_slope > 0) {
            result = setSlope(result, _slope);
        }
        if (_cliff > 0) {
            result = setCliff(result, _cliff);
        }
        return result;
    }

}
/**
  * Line describes a linear function, how the user's voice decreases from point (start, bias) with speed slope
  * BrokenLine - a curve that describes the curve of the change in the sum of votes of several users
  * This curve starts with a line (Line) and then, at any time, the slope can be changed.
  * All slope changes are stored in slopeChanges. The slope can always be reduced only, it cannot increase,
  * because users can only run out of lockup periods.
  **/

library LibBrokenLine {
    using SignedSafeMathUpgradeable for int;
    using SafeMathUpgradeable for uint;
    using LibIntMapping for mapping(uint => int);
    using safeU64 for uint;
/*
    struct Line {
        uint start;
        uint bias;
        uint slope;
    }

    struct LineData {//all data about line
        Line line;
        uint cliff;
    }
    
    struct BrokenLine {
        mapping(uint => int) slopeChanges;          //change of slope applies to the next time point
        mapping(uint => int) biasChanges;           //change of bias applies to the next time point
        mapping(uint => LineData) initiatedLines;   //initiated (successfully added) Lines
        Line initial;
    }
    */

    //line = uint256 = uint64 + uint64 + uint64 + uint64
    //start + slope

    struct BrokenLine{
        mapping(uint => int) slopeChanges;          //change of slope applies to the next time point
        mapping(uint => uint) initiatedLines; //initiated (successfully added) Lines
        uint[] history;                            //history
        uint initial;
    }

    /**
     * @dev Add Line, save data in LineData. Run update BrokenLine, require:
     *      1. slope != 0, slope <= bias
     *      2. line not exists
     **/
    function add(BrokenLine storage brokenLine, uint id, uint line, uint cliff) internal {
        uint lineWithCliff = line.setCliff(cliff);
        require(line.slope() != 0, "Slope == 0, unacceptable value for slope");
        require(line.slope() <= line.bias(), "Slope > bias, unacceptable value for slope");
        //require(brokenLine.initiatedLines[id].line.bias == 0, "Line with given id is already exist");
        require(brokenLine.initiatedLines[id].bias() == 0, "Line with given id is already exist");
        //brokenLine.initiatedLines[id] = LineData(line, cliff);
        brokenLine.initiatedLines[id] = lineWithCliff;

        update(brokenLine, line.start());
        brokenLine.initial = brokenLine.initial.setBias(brokenLine.initial.bias().add(line.bias()));
        //save bias for history in line.start minus one
        uint256 lineStartMinusOne = line.start().sub(1);
        //period is time without tail
        uint period = line.bias().div(line.slope());

        if (cliff == 0) {
            //no cliff, need to increase brokenLine.initial.slope write now
            //brokenLine.initial.slope = brokenLine.initial.slope.add(line.slope);
            brokenLine.initial = brokenLine.initial.setSlope(brokenLine.initial.slope().add(line.slope()));
            //no cliff, save slope in history in time minus one
            brokenLine.slopeChanges.addToItem(lineStartMinusOne, safeInt(line.slope()));
        } else {
            //cliffEnd finish in lineStart minus one plus cliff
            uint cliffEnd = lineStartMinusOne.add(cliff);
            //save slope in history in cliffEnd 
            brokenLine.slopeChanges.addToItem(cliffEnd, safeInt(line.slope()));
            period = period.add(cliff);
        }

        int mod = safeInt(line.bias().mod(line.slope()));
        uint256 endPeriod = line.start().add(period);
        uint256 endPeriodMinus1 = endPeriod.sub(1);
        brokenLine.slopeChanges.subFromItem(endPeriodMinus1, safeInt(line.slope()).sub(mod));
        brokenLine.slopeChanges.subFromItem(endPeriod, mod);


        // !!! TESTING SNAPSHOTTING
        brokenLine.history.push(123);
    }

    /**
     * @dev Remove Line from BrokenLine, return bias, slope, cliff. Run update BrokenLine.
     **/
    function remove(BrokenLine storage brokenLine, uint id, uint toTime) internal returns (uint bias, uint slope, uint cliff) {
        //LineData memory lineData = brokenLine.initiatedLines[id];
        uint lineData = brokenLine.initiatedLines[id];
        require(lineData.bias() != 0, "Removing Line, which not exists");
        //Line memory line = lineData.line;

        update(brokenLine, toTime);
        //check time Line is over
        bias = lineData.bias();
        slope = lineData.slope();
        cliff = 0;
        //for information: bias.div(slope) - this`s period while slope works
        uint finishTime = lineData.start().add(bias.div(slope)).add(lineData.cliff());
        if (toTime > finishTime) {
            bias = 0;
            slope = 0;
            return (bias, slope, cliff);
        }
        uint finishTimeMinusOne = finishTime.sub(1);
        uint toTimeMinusOne = toTime.sub(1);
        int mod = safeInt(bias.mod(slope));
        uint cliffEnd = lineData.start().add(lineData.cliff()).sub(1);
        if (toTime <= cliffEnd) {//cliff works
            cliff = cliffEnd.sub(toTime).add(1);
            //in cliff finish time compensate change slope by oldLine.slope
            brokenLine.slopeChanges.subFromItem(cliffEnd, safeInt(slope));
            //in new Line finish point use oldLine.slope
            brokenLine.slopeChanges.addToItem(finishTimeMinusOne, safeInt(slope).sub(mod));
        } else if (toTime <= finishTimeMinusOne) {//slope works
            //now compensate change slope by oldLine.slope
            //brokenLine.initial.slope = brokenLine.initial.slope.sub(slope);
            brokenLine.initial = brokenLine.initial.setSlope(brokenLine.initial.slope().sub(slope));
            //in new Line finish point use oldLine.slope
            brokenLine.slopeChanges.addToItem(finishTimeMinusOne, safeInt(slope).sub(mod));
            bias = finishTime.sub(toTime).mul(slope).add(uint(mod));
            //save slope for history
            brokenLine.slopeChanges.subFromItem(toTimeMinusOne, safeInt(slope));
        } else {//tail works
            //now compensate change slope by tail
            //brokenLine.initial.slope = brokenLine.initial.slope.sub(uint(mod));
            brokenLine.initial = brokenLine.initial.setSlope(brokenLine.initial.slope().sub(uint(mod)));
            bias = uint(mod);
            slope = bias;
            //save slope for history
            brokenLine.slopeChanges.subFromItem(toTimeMinusOne, safeInt(slope));
        }
        brokenLine.slopeChanges.addToItem(finishTime, mod);
        //brokenLine.initial.bias = brokenLine.initial.bias.sub(bias);
        brokenLine.initial = brokenLine.initial.setBias(brokenLine.initial.bias().sub(bias));
        //brokenLine.initiatedLines[id].line.bias = 0;
        brokenLine.initiatedLines[id] = brokenLine.initiatedLines[id].setBias(0);
        //save bias for history
    }

    /**
     * @dev Update initial Line by parameter toTime. Calculate and set all changes
     **/
    function update(BrokenLine storage brokenLine, uint toTime) internal {
        uint time = brokenLine.initial.start();
        if (time == toTime) {
            return;
        }
        uint slope = brokenLine.initial.slope();
        uint bias = brokenLine.initial.bias();
        if (bias != 0) {
            require(toTime > time, "can't update BrokenLine for past time");
            while (time < toTime) {
                bias = bias.sub(slope);

                int newSlope = safeInt(slope).add(brokenLine.slopeChanges[time]);
                require(newSlope >= 0, "slope < 0, something wrong with slope");
                slope = uint(newSlope);

                time = time.add(1);
            }
        }
        //brokenLine.initial.start = toTime;
        //brokenLine.initial.bias = bias;
        //brokenLine.initial.slope = slope;
        uint result;
        result = result.setStart(toTime);
        result = result.setBias(bias);
        result = result.setSlope(slope);
        brokenLine.initial = result;
    }

    function actualValue(BrokenLine storage brokenLine, uint toTime) internal view returns (uint) {
        uint fromTime = brokenLine.initial.start();
        uint bias = brokenLine.initial.bias();
        if (fromTime == toTime) {
            return (bias);
        }

        if (toTime > fromTime) {
            return actualValueForward(brokenLine, fromTime, toTime, bias);
        }
        require(toTime > 0, "unexpected past time");
        return actualValueBack(brokenLine, fromTime, toTime, bias);
    }

    function actualValueForward(BrokenLine storage brokenLine, uint fromTime, uint toTime, uint bias) internal view returns (uint) {
        if ((bias == 0)){
            return (bias);
        }
        uint slope = brokenLine.initial.slope();
        uint time = fromTime;

        while (time < toTime) {
            bias = bias.sub(slope);

            int newSlope = safeInt(slope).add(brokenLine.slopeChanges[time]);
            require(newSlope >= 0, "slope < 0, something wrong with slope");
            slope = uint(newSlope);

            time = time.add(1);
        }
        return bias;
    }

    function actualValueBack(BrokenLine storage brokenLine, uint fromTime, uint toTime, uint bias) internal view returns (uint) {
        
        return 0;
    }

    function safeInt(uint value) pure internal returns (int result) {
        require(value < 2**255, "int cast error");
        result = int(value);
    }
}
