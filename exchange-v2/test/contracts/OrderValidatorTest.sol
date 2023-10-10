// SPDX-License-Identifier: MIT

pragma solidity 0.7.6;
pragma abicoder v2;

import "../../contracts/OrderValidator.sol";

contract OrderValidatorTest is OrderValidator {
    function __OrderValidatorTest_init() external initializer {
        __OrderValidator_init_unchained();
    }

    function validateOrderTest(LibOrder.Order calldata order, bytes calldata signature) public view returns (bool) {
        validate(order, signature);
        return true;
    }

    function hashTypedDataV4(LibOrder.Order calldata order) external view virtual returns (bytes32) {
        bytes32 hash = LibOrder.hash(order);
        return _hashTypedDataV4(hash);
    }


    function domainSeparatorV4() external view returns (bytes32) {
        return _domainSeparatorV4();
    }

     function getChainId() external view returns (uint256 chainId) {
        this; // silence state mutability warning without generating bytecode - see https://github.com/ethereum/solidity/issues/2691
        // solhint-disable-next-line no-inline-assembly
        assembly {
            chainId := chainid()
        }
    } 

    function hashOrder(LibOrder.Order calldata order) external view virtual returns (bytes32) {
        return LibOrder.hash(order);
    }

    function hashAsset(LibAsset.Asset calldata asset)external view virtual returns (bytes32) {
        return LibAsset.hash(asset);
    }

    function EIP712NameHash() external virtual view returns (bytes32) {
        return _EIP712NameHash();
    }

    function EIP712VersionHash() external virtual view returns (bytes32) {
        return _EIP712VersionHash();
    }
}
