import axios from 'axios';
import { Projeto } from '../interfaces/Projeto';
import { ProjetoInsert } from '../interfaces/ProjetoInsert';
import { Funcionario } from '../interfaces/Funcionario';

const API_URL = 'https://localhost:7054/api';

export const fetchProjetos = async (page: number = 1, size: number = 5) => {
    try {
        const response = await axios.get(`${API_URL}/projetos`, {
        params: { page, size },
        });
        return response.data;
    } catch (error) {
        console.error('Erro ao carregar os projetos:', error);
        throw error;
    }
};

export const excluirProjeto = async (id: number) => {
    try {
        await axios.delete(`${API_URL}/projetos/${id}`);
        alert('Projeto excluído com sucesso!');
    } catch (error) {
        alert(`Erro ao excluir projeto - ${(error as any).response.data.errors[0]}`);
        throw error;
    }
};

export const atualizarProjeto = async (id: number, projeto: ProjetoInsert) => {
    const response = await axios.put(`${API_URL}/projetos/${id}`, projeto);
    return response.data;
};

export const inserirProjeto = async (projeto: ProjetoInsert): Promise<Projeto> => {
    try {
        const response = await axios.post(`${API_URL}/projetos`, projeto);
        return response.data;
    } catch (error) {
        console.error('Erro ao inserir projeto:', error);
        throw error;
    }
};

export const vincularFuncionarios = async (projetoId: number, idsFuncionarios: string) => {
    try {
        await axios.post(`${API_URL}/projetos/${projetoId}/vincular-funcionarios`, null, {
            params: { idsFuncionarios }
        });
        alert('Funcionários vinculados com sucesso!');
    } catch (error) {
        console.error('Erro ao vincular funcionários:', error);
        alert('Erro ao vincular funcionários.');
    }
};