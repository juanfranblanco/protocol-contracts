// SPDX-License-Identifier: MIT

pragma solidity 0.7.6;
pragma abicoder v2;

import "../../contracts/libs/LibBrokenLine.sol";

contract BrokenLineTest {
    LibBrokenLine.BrokenLine public brokenLineTestLocal;
    using safeU64 for uint;
    event resultRemoveLine(uint bias, uint slope, uint cliff);

    function addTest(uint start, uint bias, uint slope, uint id, uint cliff) public {
        uint curLine = safeU64.create(start, bias, slope, 0);
        LibBrokenLine.add(brokenLineTestLocal, id, curLine, cliff);
    }

    function update(uint timeTo) public {
        LibBrokenLine.update(brokenLineTestLocal, timeTo);
    }

    function getCurrent() view public returns (uint) {
        return brokenLineTestLocal.initial;
    }

    function getActualValue(uint timeTo) external returns (uint bias) {
        return LibBrokenLine.actualValue(brokenLineTestLocal, timeTo);
    }

    function removeTest(uint id, uint toTime) public {
        (uint bias, uint slope, uint cliff) = LibBrokenLine.remove(brokenLineTestLocal, id, toTime);
        emit resultRemoveLine(bias, slope, cliff);
    }

    uint public asdsadas;

    function createUint(uint _start, uint _bias, uint _slope, uint _cliff) external {
      asdsadas = safeU64.create(_start, _bias, _slope, _cliff);
    }

    function getStart() external view returns(uint) {
      return asdsadas.start();
    }

    function getBias() external view returns(uint) {
      return asdsadas.bias();
    }

    function getSlope() external view returns(uint) {
      return asdsadas.slope();
    }

    function getCliff() external view returns(uint) {
      return asdsadas.cliff();
    }
}
