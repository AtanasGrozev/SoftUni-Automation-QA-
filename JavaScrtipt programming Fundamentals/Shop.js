function shop(products = [], args){

    let numbersOfProduct = parseInt(products[0]);
    let productsArray = products.slice(1,numbersOfProduct +1);    

let splitCommands = products.slice(numbersOfProduct +1)

for (let index = 0; index < splitCommands.length; index++) {
    const rawParam = splitCommands[index].split(' ');
    let command = rawParam[0];

    if (command == "Add"){

        productsArray.push(rawParam[1])      

    }
    else if(command == "Sell")
    {
        console.log( `${productsArray[0]} product sold!`)
        productsArray.shift(productsArray[0])        

    }
    else if(command == "Swap"){
        let firstNumber = rawParam[1];
        let secondNumber = rawParam[2];
        let temp = productsArray[firstNumber] 
         productsArray[firstNumber] = productsArray[secondNumber] 
         productsArray[secondNumber] = temp 
         console.log("Swapped!")
    }
    else if(command == "End"){
        if (productsArray == productsArray.length){             
           console.log("The shop is empty")
 }
         else 

           console.log(`Products left: ${productsArray.join(", ")}`)          
    }

}   

}
shop([])
//shop(['5', 'Milk', 'Eggs', 'Bread','Cheese', 'Butter', 'Add Yogurt', 'Swap 1 4', 'End'])
//shop(['3', 'Shampoo', 'Soap', 'Toothpaste', 'Sell', 'Sell', 'Sell', 'End'])