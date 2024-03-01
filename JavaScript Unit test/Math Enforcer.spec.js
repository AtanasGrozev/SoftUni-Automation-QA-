import { mathEnforcer } from "./Math Enforcer.js"
import { expect } from "chai"

describe('mathEnforcer', () => {

    describe('addFive', () => {
        it('should return undefined when pass strings as input', () => {
            //Arrange
            let input = "1";


            //Act
            let result = mathEnforcer.addFive(input)

            //Assert
            expect(result).to.be.undefined


        })
        it('should return correct value with input number', () => {
            //Arrange
            let input = 1;


            //Act
            let result = mathEnforcer.addFive(input)

            //Assert
            expect(result).to.be.equals(6)
        })
        it('should return correct value with negative input number', () => {
            //Arrange
            let input = -5;
            let undefinedInput = undefined


            //Act
            let result = mathEnforcer.addFive(input)
            let result2 = mathEnforcer.addFive(undefinedInput)

            //Assert
            expect(result).to.be.equals(0)
            expect(result2).to.be.undefined
        })
        it('should return correct value when pass floating number as input', () => {
            //Arrange
            let floatingInput = 5.5;
            let floatingNegativeInput = -5.5


            //Act
            let result = mathEnforcer.addFive(floatingInput)
            let result2 = mathEnforcer.addFive(floatingNegativeInput)

            //Assert
            expect(result).to.be.closeTo(10.5, 0.01)
            expect(result2).to.be.closeTo(-0.5, 0.01)
        })

    })
    describe('subtractTen', () => {
        it('should return undefined when pass strings as input', () => {
            //Arrange
            let input = "1";


            //Act
            let result = mathEnforcer.subtractTen(input)

            //Assert
            expect(result).to.be.undefined


        })
        it('should return correct value with subtract with 10', () => {
            //Arrange
            let input = 30;


            //Act
            let result = mathEnforcer.subtractTen(input)

            //Assert
            expect(result).to.be.equals(20)


        })   
        it('should return correct value with negative subtract with 10', () => {
            //Arrange
            let input = -30;


            //Act
            let result = mathEnforcer.subtractTen(input)

            //Assert
            expect(result).to.be.equals(-40)


        }) 
        it('should return correct value when pass floating number as input', () => {
            //Arrange
            let floatingInput = 5.5;
            let floatingNegativeInput = -5.5


            //Act
            let result = mathEnforcer.subtractTen(floatingInput)
            let result2 = mathEnforcer.subtractTen(floatingNegativeInput)

            //Assert
            expect(result).to.be.closeTo(-4.5, 0.01)
            expect(result2).to.be.closeTo(-15.5, 0.01)
        })  

    })
    describe('sum', () => {
        it('should return undefined when input1 is string', () => {
            //Arrange
            let input1 = "1";
            let input2 = 0;


            //Act
            let result = mathEnforcer.sum(input1,input2)

            //Assert
            expect(result).to.be.undefined


        })
        it('should return undefined when input2 is string', () => {
            //Arrange
            let input1 = 1;
            let input2 = "1";


            //Act
            let result = mathEnforcer.sum(input1,input2)

            //Assert
            expect(result).to.be.undefined


        })
        it('should return correct sum with positive numbers', () => {
            //Arrange
            let input1 = 1;
            let input2 = 100;


            //Act
            let result = mathEnforcer.sum(input1,input2)

            //Assert
            expect(result).to.be.equals(101)


        })
        it('should return correct value with negative numbers', () => {
            //Arrange
            let input1 = -1;
            let input2 = -100;


            //Act
            let result = mathEnforcer.sum(input1,input2)

            //Assert
            expect(result).to.be.equals(-101)


        })
        it('should return correct value with negative numbers', () => {
            //Arrange
            let input1 = 1;
            let input2 = -100;


            //Act
            let result = mathEnforcer.sum(input1,input2)

            //Assert
            expect(result).to.be.equals(-99)


        })
        it('should return correct value with floating  numbers', () => {
            //Arrange
            let input1 = 5.5;
            let input2 = -5.5;


            //Act
            let result = mathEnforcer.sum(input1,input2)

            //Assert
            expect(result).to.be.closeTo(0, 0.00)


        })




    })
}
)