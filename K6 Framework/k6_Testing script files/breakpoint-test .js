import http from "k6/http"
import {sleep} from "k6"

export const options = {
    stages: [
        
        {
            duration: "4h",
            target : 100000
        }
        
       
    ]
}

export default function(){

    http.get("https://test.k6.io")
    sleep(1)
   
}