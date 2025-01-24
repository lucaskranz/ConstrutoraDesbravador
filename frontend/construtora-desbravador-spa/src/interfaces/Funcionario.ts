import { Projeto } from "./Projeto";

export interface Funcionario {
    id: number;
    nome: string;
    sobrenome: string;
    email: string;
    projetosVinculados: Projeto[];
    projetosResponsavel: Projeto[];
}

