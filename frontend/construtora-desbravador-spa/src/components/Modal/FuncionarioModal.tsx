import React from 'react';
import { Funcionario } from '../../interfaces/Funcionario';

interface FuncionarioModalProps {
    funcionario: Funcionario | null;
    showModal: boolean;
    onClose: () => void;
}

const FuncionarioModal: React.FC<FuncionarioModalProps> = ({ funcionario, showModal, onClose }) => {
    return (
        <div className={`modal fade ${showModal ? 'show' : ''}`} id="modalDetalhes" tabIndex={-1} aria-labelledby="modalDetalhesLabel" aria-hidden={!showModal}>
            <div className="modal-dialog">
                <div className="modal-content">
                <div className="modal-header">
                    <h5 className="modal-title" id="modalDetalhesLabel">Detalhes do Funcionário</h5>
                    <button type="button" className="btn-close" onClick={onClose} data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div className="modal-body">
                    {funcionario ? (
                    <div>
                        <h6><strong>Id:</strong> {funcionario.id}</h6>
                        <h6><strong>Nome:</strong> {funcionario.nome}</h6>
                        <h6><strong>Email:</strong> {funcionario.email}</h6>
                        <hr />
                        <h5>Projetos em que está vinculado:</h5>
                        <ul>
                            {funcionario.projetosVinculados.map((projeto, index) => (
                            <li key={index}>{projeto.nome}</li>
                            ))}
                        </ul>
                        <hr />
                        <h5>Projetos que é responsável:</h5>
                        <ul>
                            {funcionario.projetosResponsavel.map((projeto, index) => (
                            <li key={index}>{projeto.nome}</li>
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

export default FuncionarioModal;