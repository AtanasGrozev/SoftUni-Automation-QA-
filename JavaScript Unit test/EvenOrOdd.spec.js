import { isOddOrEven } from './EvenOrOdd.js'
import { expect } from 'chai'

describe('isOddOrEven', () => {

    it ('should return undefined if input not string', () => {
        //Arrange
        let input = 11.5;

        //Act
        let result = isOddOrEven(input)

        //Assert
        expect(result).to.be.undefined


    })
    it ('should return undefined if input not string', () => {
        //Arrange
        let input = true;

        //Act
        let result = isOddOrEven(input)

        //Assert
        expect(result).to.be.undefined


    })
    
    it ('should return Odd if input is Odd number', () => {
      //Arrange
      let input = 'Petar';

      //Act
      let result = isOddOrEven(input)

      //Assert
      expect(result).to.equals("odd")

    })
    it ('should return even if input is even number', () => {
        //Arrange
        let input = 'Geri';
  
        //Act
        let result = isOddOrEven(input)
  
        //Assert
        expect(result).to.equals("even")
  
      })


});
