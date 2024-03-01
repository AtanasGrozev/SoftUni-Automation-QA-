import { expect } from 'chai'
import { analyzeArray } from './ArrayAnalyzer.js'

describe('analyzeArray', () => {

    it('should return undefined when pass non-array as input', () => {
        const nonArrayInput = "somestring";

        const undefinedResult = analyzeArray(nonArrayInput)

        expect(undefinedResult).to.be.undefined;


    })
    it('should return undefined when pass empty array as input', () => {
        const emptyArray = [];

        const emtyResult = analyzeArray(emptyArray)

        expect(emtyResult).to.be.undefined


    })
    it('should return correct value when pass array with different numbers as input ', () => {
        const correctArray = [1,2,3,4,6];

        const correctResult = analyzeArray(correctArray)

        expect(correctResult).to.deep.equals({min: 1, max: 6, length: 5 })


    })
    it('should return correct value when pass array with single element as input ', () => {
        const correctArray = [1];

        const correctResult = analyzeArray(correctArray)

        expect(correctResult).to.deep.equals({min: 1, max: 1, length: 1 })


    })
    it('should return correct value when pass array with same elements as input ', () => {
        const correctArray = [1,1,1,1];

        const correctResult = analyzeArray(correctArray)

        expect(correctResult).to.deep.equals({min: 1, max: 1, length: 4 })


    })
    it('should return correct value when pass array with negative  elements as input ', () => {
        const correctArray = [-1,-1,-1,-1];

        const correctResult = analyzeArray(correctArray)

        expect(correctResult).to.deep.equals({min: -1, max: -1, length: 4 })


    })
    it('should return correct value when pass array with floating point numbers  as input ', () => {
        const correctArray = [-1.5,-1,-3.9,-5.5];

        const correctResult = analyzeArray(correctArray)

        expect(correctResult).to.deep.equals({min: -5.5, max: -1, length: 4 })


    })


})
