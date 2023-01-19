// SPDX-License-Identifier: MIT

pragma solidity 0.7.6;
pragma abicoder v2;

import "./LockingBase.sol";

abstract contract LockingRelock is LockingBase {
    using SafeMathUpgradeable for uint;
    using LibBrokenLine for LibBrokenLine.BrokenLine;


    uint256[50] private __gap;
}
