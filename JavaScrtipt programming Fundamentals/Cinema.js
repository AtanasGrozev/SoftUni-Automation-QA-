function cinema(inputArray = []) {
    var MovieArray = [];    

    let numbersOfMovie = parseInt(inputArray[0]);
    for (let index = 1; index <= numbersOfMovie; index++) {
        var movie = inputArray[index];
        MovieArray.push(movie);        
    }

    // Adjust the slice index to correctly include all commands
    let commandsName = inputArray.slice(numbersOfMovie + 1);

    for (let index = 0; index < commandsName.length; index++) {
        const rawParam = commandsName[index].split(' ');
        let command = rawParam[0];
        
        if (command === "Sell") {
            let soldMovie = MovieArray.shift();
            console.log(`${soldMovie} ticket sold!`); // Corrected template literal syntax
        } else if (command === "Add") {
            let movieTitleToAdd = rawParam.slice(1).join(' '); // Join the rest of the rawParam to get full movie title
            MovieArray.push(movieTitleToAdd);
        } else if (command === "Swap") {
            let firstIndex = parseInt(rawParam[1]); // Corrected syntax
            let secondIndex = parseInt(rawParam[2]); // Corrected syntax
            // Corrected variable references and added swap logic with a temp variable
            if (firstIndex < 0 || firstIndex >= MovieArray.length || secondIndex < 0 || secondIndex >= MovieArray.length) {
                continue;
            }
            let temp = MovieArray[firstIndex];
            MovieArray[firstIndex] = MovieArray[secondIndex];
            MovieArray[secondIndex] = temp;
            console.log("Swapped!");
        } else if (command === "End") {
            break;
        }
    }
   
    if (MovieArray.length) {
        console.log(`Tickets left: ${MovieArray.join(", ")}`);
    } else {
        console.log("The box office is empty");
    }
}

cinema(['5', 'The Matrix', 'The Godfather', 'The Shawshank Redemption',
 'The Dark Knight', 'Inception', 'Add The Lord of the Rings', 'Swap -1 -1', 'End'])