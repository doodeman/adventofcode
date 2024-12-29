import * as fs from 'fs'; 
import Computer from './class/computer'; 

solveDay1();
solveDay2();

function solveDay1() : void {
    let computer = readInput(); 
    computer.runProgram();
    console.log(computer.getOutput());
}

function solveDay2() : void {
    let computer = readInput(); 

    let desiredOutput = computer.Program.join(','); 

    let counter = 0; 
    while (true) {
        computer.A = counter; 
        computer.B = 0; 
        computer.C = 0; 
        computer.Output = [];
        computer.Instruction = 0;
        computer.runProgram(); 
        if (computer.getOutput() == desiredOutput) {
            console.log("found!"); 
            console.log(counter); 
            return; 
        }
        counter = counter + 1;
        if (counter % 1000 == 0) {
            console.log(counter);
        }
    }
}

function readInput() : Computer { 
    const contentLines = fs.readFileSync("input_real", 'utf8').split('\r\n'); 

    let registerA: number = 0; 
    let registerB: number = 0; 
    let registerC: number = 0; 
    let program: number[] = [];
    contentLines.forEach(line => {
        if (line === '') {
            return; 
        }
        if (line.substring(0, 11) === 'Register A:') {
            registerA = parseInt(line.substring(12)); 
        }
        if (line.substring(0, 11) === 'Register B:') {
            registerB = parseInt(line.substring(12)); 
        }
        if (line.substring(0, 11) === 'Register C:') {
            registerC = parseInt(line.substring(12)); 
        }
        if (line.substring(0, 8) === 'Program:') {
            program = line.substring(9).split(',')
                                       .map(x => parseInt(x));
        }
    });
    let ret = new Computer(registerA, registerB, registerC, program);
    return ret;
}