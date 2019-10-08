//configs
const skipChar = '"';
const seperatorChar = ';';

export function convertCsvLinesToObject(lines){
    //first line must be column names
    const names = lines[0];

    let objs = [];

    for(let i = 1; i < lines.length; i ++){
        let obj = {};
        for(let c = 0 ; c < names.length; c ++){
            if(names[c] == '') continue;

            obj[names[c]] = lines[i][c];
        }

        objs.push(obj);
    }

    return objs;
}

export function parseCsv(csv){
    //lets start processing the csv
    let buffer = '';
    let lines = [];
    let colums = [];
    let isSkipping = false;

    //scarn each letter and process them
    for(let i = 0; i < csv.length; i ++){
        let char = csv[i];

        if(char == '﻿') continue; //skip utf-8 char
        
        if(isSkipping){
            if(char == skipChar){
                isSkipping = false;
            } else {
                buffer += char;
            }
        } else {
            if(char == skipChar){
                isSkipping = true;
            } else {
                if(char == seperatorChar){
                    colums.push(buffer);
                    buffer = '';
                } else if (char == '\n'){
                    if(buffer.length > 0){
                        colums.push(buffer);
                        buffer = '';
                    } 

                    lines.push(colums);
                    colums = [];
                } else {
                    buffer += char;
                }
            }
        }
    }

    if(colums.length > 0){
        lines.push(colums);
    }

    return convertCsvLinesToObject(lines);
}