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
    } catch (error) {
        console.error("Erro ao inserir aleatórios", error);
        throw error;
    }
  };
