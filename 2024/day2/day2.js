const fs = require('fs').promises;

async function parseNumberFile(filePath) {
  try {
    const data = await fs.readFile(filePath, 'utf8');
    return data
      .trim()
      .split('\n')
      .map(line => line.split(' ').map(Number));
  } catch (error) {
    throw error;
  }
}

function lessThan(x, y) {
    return x < y; 
}

function greaterThan(x, y) {
    return x > y; 
}

function absDiffIsBetween3And1(x, y) {
    return Math.abs(x - y) <= 3;
}

/*
iterates through list, and returns true if every element in list satisfies condition of 
compareFunc or is last element in list
*/
function iterateAndCompareWithNextFunction(input, compareFunc) {
    var iterateResult = input.map((x, i, list) => {
        //if last element
        if (i === list.length - 1) 
            return true; 
        if (compareFunc(x, list[i+1]))
            return true; 
        return false; 
    });
    return iterateResult.every(x => x);
}

function isSafe(input) {
    var isDescending = iterateAndCompareWithNextFunction(input, greaterThan);
    var isAscending = iterateAndCompareWithNextFunction(input, lessThan);
    var isDifferenceAlwaysBetween1And3 = iterateAndCompareWithNextFunction(input, absDiffIsBetween3And1);
    var isSafe = ( isDescending || isAscending ) && isDifferenceAlwaysBetween1And3;
    return isSafe; 
}

async function main() {
    var numbers = await parseNumberFile("input");
    var safeCount = numbers.map(x => {
        return isSafe(x, false);
    }).filter(x => x).length;
    console.log("Safe sequences: " + safeCount);

}

main(); 