function library(books = []){
    let numberOfBooks = parseInt(books[0]);
    let libraryBooks = books.slice(1 , numberOfBooks+1); // масив от книги 
    let commnadsArray = books.slice(numberOfBooks +1,); 

    
    for (let index = 0; index < commnadsArray.length; index++) {
        const rawParam = commnadsArray[index].split(' ');
        let command = rawParam[0];   
        const returnMovie = rawParam.slice(1) 
        const joinReturnMovie = returnMovie.join(" ")  


        if ( command == "Lend"){
            
        console.log(`${libraryBooks[0]} book lent!`)
          libraryBooks.shift();       

        }
        else if (command == "Return"){
            libraryBooks.unshift(joinReturnMovie);
            // Трябва ли да се изпечата на конзолата?
        }
        else if (command == "Exchange"){
            let startNumber = rawParam[1];
            let secondNumber = rawParam[2]
            let temp = libraryBooks[startNumber] 
            libraryBooks[startNumber] = libraryBooks[secondNumber] 
            libraryBooks[secondNumber] = temp 
            console.log("Exchanged!")
        }
        else if(command == "Stop"){

            if (libraryBooks.length == 0){

                console.log("The library is empty")
                
            }
            else {
                console.log(`Books left: ${libraryBooks.join(", ")}`)
            }
          
            
        }
    }   
            
}
library(['3', 'The Da Vinci Code', 'The Girl with the Dragon Tattoo', 'The Kite Runner', 'Lend', 'Lend', 'Lend', 'Stop'])
//library(['3', 'Harry Potter', 'The Lord of the Rings', 'The Hunger Games', 'Lend', 'Stop', 'Exchange 0 1'])
//library(['5', 'The Catcher in the Rye', 'To Kill a Mockingbird', 'The Great Gatsby', '1984', 'Animal Farm', 'Return Brave New World', 'Exchange 1 4', 'Stop'])
