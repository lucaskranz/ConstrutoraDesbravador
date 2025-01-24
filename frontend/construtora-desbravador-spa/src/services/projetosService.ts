import axios from 'axios';
import { Projeto } from '../interfaces/Projeto';

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