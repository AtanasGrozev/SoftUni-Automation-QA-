import { lookupChar } from './Char Lookup.js'
import { expect } from 'chai'

describe('LookupChar', () => {

    it('return undefined if first parameter is not string', () => {
        //arrange
        let firstParameter = 1;
        let index = 3
        //act
        let result = lookupChar(firstParameter, index)
        //assert
        expect(result).to.be.undefined

    })
    it('return undefined if second parameter is not a number', () => {
        //arrange
        let firstParameter = 'pesho';
        let index = "asd"
        //act
        let result = lookupChar(firstParameter, index)
        //assert
        expect(result).to.be.undefined

    })
    it('return correct index if both parameters are correct = string and number', () => {
        //arrange
        let firstParameter = 'pesho';
        let index = 3
        //act
        let result = lookupChar(firstParameter, index)
        //assert
        expect(result).to.be.equals("h")

    })
    it('return  Incorrect index if index is bigger then string length', () => {
        //arrange
        let firstParameter = 'pesho';
        let index = 6
        //act
        let result = lookupChar(firstParameter, index)
        //assert
        expect(result).to.be.equals("Incorrect index")

    })
    it('return undefined with correct string and index with floating point', () => {
        //arrange
        let firstParameter = 'pesho';
        let index = 3.5
        //act
        let result = lookupChar(firstParameter, index)
        //assert
        expect(result).to.be.undefined

    })
    it('return Incorrect index with correct string and negative number', () => {
        //arrange
        let firstParameter = 'pesho';
        let index = -3
        //act
        let result = lookupChar(firstParameter, index)
        //assert
        expect(result).to.be.equals('Incorrect index')

    })
})
