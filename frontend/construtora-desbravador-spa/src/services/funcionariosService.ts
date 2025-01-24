import axios from 'axios';
import { Funcionario } from '../interfaces/Funcionario';
import { PaginacaoResult } from '../interfaces/PaginacaoResult';
import { API_URL } from '../config/config';

export const getFuncionarios = async (page: number, size: number): Promise<PaginacaoResult<Funcionario>> => {
    try {
        const response = await axios.get(`${API_URL}/funcionarios`, {
            params: {
                page: page,
                size: size
            }
        });

        return response.data; 
    } catch (error) {
        console.error('Error fetching funcionarios:', error);
        throw error;
    }
};

export const inserirAleatorios = async () => {
    try {
        await axios.post(`${API_URL}/funcionarios/adicionar-aleatorios`);
        alert('Funcionário aleatórios inseridos com sucesso!');
    } catch (error) {
        console.error(`Erro ao inserir aleatórios - ${error}`, error);
        throw error;
    }
};

export const excluirFuncionario = async (id: number) => {
    try {
        await axios.delete(`${API_URL}/funcionarios/${id}`);
        alert('Funcionário excluído com sucesso!');
    } catch (error) {
        alert(`Erro ao excluir funcionário - ${(error as any).response.data.errors[0]}`);
        throw error;
    }
};
