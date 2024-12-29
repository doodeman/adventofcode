import Computer from '../class/computer'; 


test('Test case 1', () => {
    let computer = new Computer(0, 0, 9, [2,6]);
    computer.runProgram();
    expect(computer.B).toBe(1);
});

test('Test case 2', () => {
    let computer = new Computer(10, 0, 0, [5,0,5,1,5,4]);
    computer.runProgram(); 
    let output = computer.getOutput(); 
    expect(output).toEqual('0,1,2');
});

test('Test case 3', () => {
    let computer = new Computer(2024, 0, 0, [0,1,5,4,3,0]);
    computer.runProgram(); 
    let output = computer.getOutput(); 
    expect(output).toEqual('4,2,5,6,7,7,7,7,3,1,0');
    expect(computer.A).toBe(0);
});

test('Test case 4', () => {
    let computer = new Computer(0, 29, 0, [1,7]);
    computer.runProgram(); 
    expect(computer.B).toEqual(26); 
});

test('Test case 5', () => {
    let computer = new Computer(0, 2024, 43690, [4,0]);
    computer.runProgram(); 
    expect(computer.B).toEqual(44354);
});

test('Test case 6', () => {
    let computer = new Computer(729, 0, 0, [0,1,5,4,3,0]);
    computer.runProgram();
    expect(computer.getOutput()).toEqual('4,6,3,5,6,3,5,2,1,0');
});

test('Test bxl', () => {
    let computer = new Computer(0, 5, 0, [1, 3]);
    computer.runProgram(); 
    expect(computer.B).toEqual(6);
});