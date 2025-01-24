import { RiscoProjetoEnum } from "../enums/RiscoProjetoEnum";
import { StatusProjetoEnum } from "../enums/StatusProjetoEnum";
import { Funcionario } from "./Funcionario";

export interface Projeto {
    id: number;
    nome: string;
    descricao: string;
    dataInicio: Date;
    dataTermino: Date;
    statusProjeto: StatusProjetoEnum;
    riscoProjeto: RiscoProjetoEnum;
    responsavel: Funcionario;
    funcionarios: Funcionario[]
}