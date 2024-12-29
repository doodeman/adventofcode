export default class Computer {
    public A: number; 
    public B: number; 
    public C: number; 
    public Program: number[]; 
    public Instruction: number; 
    public Output: number[]; 

    constructor(a: number, b: number, c: number, program: number[]) {
        this.A = a; 
        this.B = b; 
        this.C = c; 
        this.Program = program; 
        this.Instruction = 0;
        this.Output = [];  
    }

    runProgram() {
        while (this.Instruction < this.Program.length) {
            let opcode = this.Program[this.Instruction];
            let operand = this.Program[this.Instruction + 1];
            this.doInstruction(opcode, operand);
            this.Instruction = this.Instruction + 2; 
        }
    }

    getOutput() : string {
        return this.Output.join(',');
    }

    doInstruction(opcode: number, operand: number) {
        switch(opcode) {
            case 0: {
                this.adv(operand); 
                break;
            }
            case 1: {
                this.bxl(operand); 
                break;
            }
            case 2: {
                this.bst(operand); 
                break;
            }
            case 3: {
                this.jnz(operand); 
                break;
            }
            case 4: {
                this.bxc(operand); 
                break;
            }
            case 5: {
                this.out(operand); 
                break;
            }
            case 6: {
                this.bdv(operand); 
                break;
            }
            case 7: {
                this.cdv(operand); 
                break;
            }
            default: {
                throw {
                    message: "Invalid opcode",
                    opcode: opcode
                }
            }
        }
    }

    adv(x: number) {
        this.A = this.xdv(x);
    }
    
    bxl(x: number) {
        this.B = this.B ^ x; 
    }
    
    bst(x: number) {
        this.B = this.combo(x) % 8; 
    }
    
    jnz(x: number) {
        if (this.A === 0) {
            return; 
        }
        this.Instruction = x - 2;
    }
    
    bxc(x: number) {
        this.B = this.B ^ this.C;
    }
    
    out(x: number) {
        let comb = this.combo(x); 
        let res = comb % 8; 
        this.Output.push(res);
    }
    
    bdv(x: number) {
        this.B = this.xdv(x); 
    }
    
    cdv(x: number) {
        this.C = this.xdv(x);
    }
    
    xdv(x: number) : number {
        let combo = this.combo(x); 
        let denominator = 2 ** combo; 
        let divisionResult = this.A / denominator; 
        let result = Math.floor(divisionResult);
        return result; 
    }

    combo(x: number) : number {
        if (x < 4) {
            return x; 
        }
        switch (x) {
            case 4: {
                return this.A; 
            }
            case 5: {
                return this.B; 
            }
            case 6: {
                return this.C; 
            }
            default: {
                throw {
                    message: "Invalid combo operand",
                    operandi: x
                }
            }
        }
        
    }
}