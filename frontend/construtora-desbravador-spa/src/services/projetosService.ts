import axios from 'axios';
import { Projeto } from '../interfaces/Projeto';
import { API_URL } from '../config/config';

export const fetchProjetos = async (): Promise<Projeto[]> => {
    try {
        const response = await axios.get(`${API_URL}/projetos`);
        return response.data; 
    } catch (error) {
        console.error('Erro ao buscar projetos:', error);
        throw error;
    }
};