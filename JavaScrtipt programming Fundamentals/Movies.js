function movies(commands = []){
    let movies = [];
    for (let command of commands){
        if (command.includes('addMovie')){
            let movieName = command.split('addMovie ')[1];
            movies.push({name: movieName});
        } else if (command.includes('directedBy')){
            let [movieName, director] = command.split(' directedBy ');
            let myMovie = movies.find(m => m.name === movieName);
            if (myMovie){
                myMovie.director = director;
            }
        } else if (command.includes('onDate')){
            let [movieName, date] = command.split(' onDate ');
            let myMovie = movies.find(m => m.name === movieName);
            if (myMovie){
                myMovie.date = date;
            }
        }
    }
    for (let movie of movies){
        if (movie.name && movie.director && movie.date){
            console.log(JSON.stringify(movie));
        }
    }



}

movies([

    'addMovie Fast and Furious',
    
    'addMovie Godfather',
    
    'Inception directedBy Christopher Nolan',
    
    'Godfather directedBy Francis Ford Coppola',
    
    'Godfather onDate 29.07.2018',
    
    'Fast and Furious onDate 30.07.2018',
    
    'Batman onDate 01.08.2018',
    
    'Fast and Furious directedBy Rob Cohen'
    
    ])