import React from 'react';
import { Projeto } from '../../interfaces/Projeto';

interface ProjetoModalProps {
    projeto: Projeto | null;
    showModal: boolean;
    onClose: () => void;
}

const ProjetoModal: React.FC<ProjetoModalProps> = ({ projeto, showModal, onClose }) => {
    return (
        <div className={`modal fade ${showModal ? 'show' : ''}`} id="modalDetalhes" tabIndex={-1} aria-labelledby="modalDetalhesLabel" aria-hidden={!showModal}>
            <div className="modal-dialog">
                <div className="modal-content">
                <div className="modal-header">
                    <h5 className="modal-title" id="modalDetalhesLabel">Detalhes do Projeto</h5>
                    <button type="button" className="btn-close" onClick={onClose} data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div className="modal-body">
                    {projeto ? (
                    <div>
                        <h5>{projeto.nome}</h5>
                        <p>{projeto.descricao}</p>
                        <h6><strong>Responsável: </strong>{`${projeto.responsavel.nome} ${projeto.responsavel.sobrenome}`}</h6>
                        <h6><strong>Data Início:</strong> {new Date(projeto.dataInicio).toLocaleDateString("pt-BR")}</h6>
                        <h6><strong>Data Término:</strong> {new Date(projeto.dataTermino).toLocaleDateString("pt-BR")}</h6>
                        <hr></hr>
                        <h5>Funcionários vinculados:</h5>
                        <ul>
                            {projeto.funcionarios.map((funcionario, index) => (
                            <li key={index}>{`${funcionario.nome} ${funcionario.sobrenome}`}</li>
                            ))}
                        </ul>
                    </div>
                    ) : (
                    <p>Carregando...</p>
                    )}
                </div>
                <div className="modal-footer">
                    <button type="button" className="btn btn-secondary" data-bs-dismiss="modal" onClick={onClose}>Fechar</button>
                </div>
                </div>
            </div>
        </div>
    );
};

export default ProjetoModal;