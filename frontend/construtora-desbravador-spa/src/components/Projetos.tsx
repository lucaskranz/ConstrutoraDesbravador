import React, { useEffect, useState } from 'react';
import { fetchProjetos } from '../services/projetosService';
import { Projeto } from '../interfaces/Projeto';

const Projetos: React.FC = () => {
    const [projetos, setProjetos] = useState<Projeto[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        const getProjetos = async () => {
        try {
            const data = await fetchProjetos();
            setProjetos(data);
        } catch (error) {
            console.error('Erro ao carregar projetos:', error);
        } finally {
            setLoading(false);
        }
        };

        getProjetos();
    }, []);
    
    return (
        <div className="container mt-5">
            <h1>Projetos</h1>
            {loading ? (
                <p>Carregando...</p>
            ) : (
                <ul className="list-group">
                {projetos.map((projeto) => (
                    <li key={projeto.id} className="list-group-item">
                    <h5>{projeto.nome}</h5>
                    <p>{projeto.descricao}</p>
                    </li>
                ))}
                </ul>
            )}
        </div>
      );
};

export default Projetos;