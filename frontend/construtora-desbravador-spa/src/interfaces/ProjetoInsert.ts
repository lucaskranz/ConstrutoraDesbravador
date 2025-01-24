import { RiscoProjetoEnum } from "../enums/RiscoProjetoEnum";
import { StatusProjetoEnum } from "../enums/StatusProjetoEnum";

export interface ProjetoInsert {
    id: number;
    nome: string;
    descricao: string;
    dataInicio: Date;
    dataTermino: Date;
    statusProjeto: StatusProjetoEnum;
    riscoProjeto: RiscoProjetoEnum;
    responsavelId: number;
    
}