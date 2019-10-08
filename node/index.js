import fs from "fs";
import {parseCsv} from "./csv";

//get csv path
let csv = '.' + require("../InfoJanssen.csv");

//load csv into memory
fs.readFile(csv, (err, data) => {
    if(err){
        console.error(err);
        return;
    } 

    console.log('parsing file');
    let csvObj = parseCsv(data.toString());

    console.log('writing json document');
    //lets serialize and save the document
    fs.writeFileSync('csv.json', JSON.stringify(csvObj, null, 2));

    console.log('done');
});
