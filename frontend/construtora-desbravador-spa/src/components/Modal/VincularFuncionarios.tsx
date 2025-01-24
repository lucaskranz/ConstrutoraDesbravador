import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { getFuncionarios } from '../../services/funcionariosService';
import { Funcionario } from '../../interfaces/Funcionario';
import { vincularFuncionarios } from '../../services/projetosService';

interface VincularFuncionariosProps {
    projetoId: number; // ID do projeto para vinculação
    showModal: boolean;
    onClose: () => void; // Callback para fechar o modal
    onFuncionariosVinculados: () => void; // Callback após sucesso da vinculação
}

const VincularFuncionarios: React.FC<VincularFuncionariosProps> = ({
    projetoId,
    showModal,
    onClose,
    onFuncionariosVinculados,
}) => {
    const [funcionarios, setFuncionarios] = useState<Funcionario[]>([]);
    const [selectedFuncionarioId, setSelectedFuncionarioId] = useState<number | null>(null);
    const [funcionariosVinculados, setFuncionariosVinculados] = useState<Funcionario[]>([]);

    // Busca todos os funcionários ao carregar
    useEffect(() => {
        if (showModal) {
            setFuncionariosVinculados([]);
            fetchFuncionarios();
        }
    }, [showModal]);

    const fetchFuncionarios = async () => {
        try {
            const response = await getFuncionarios(1, 1000);
            setFuncionarios(response.items);
        } catch (error) {
            console.error('Erro ao carregar funcionários:', error);
            alert('Não foi possível carregar os funcionários.');
        }
    };

    const handleAddFuncionario = () => {
        if (selectedFuncionarioId) {
            const funcionario = funcionarios.find((f) => f.id === selectedFuncionarioId);
            if (funcionario && !funcionariosVinculados.find((f) => f.id === funcionario.id)) {
                setFuncionariosVinculados((prev) => [...prev, funcionario]);
            }
        }
    };

    const handleRemoveFuncionario = (id: number) => {
        setFuncionariosVinculados((prev) => prev.filter((f) => f.id !== id));
    };

    const handleVincularFuncionarios = async () => {
        const ids = funcionariosVinculados.map((f) => f.id).join(',');
        try {
            await vincularFuncionarios(projetoId, ids);            
            onFuncionariosVinculados();
            onClose();
            
        } catch (error) {
            alert(error);
        }
    };

    if (!showModal) {
        return null;
    }

    return (
        <div className="modal show d-block" tabIndex={-1}>
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">Vincular Funcionários</h5>
                        <button type="button" className="btn-close" onClick={onClose}></button>
                    </div>
                    <div className="modal-body">
                        <div className="mb-3">
                            <label htmlFor="funcionarioSelect" className="form-label">
                                Funcionários
                            </label>
                            <div className="d-flex align-items-center">
                                <select
                                    id="funcionarioSelect"
                                    className="form-select"
                                    onChange={(e) => setSelectedFuncionarioId(Number(e.target.value))}
                                >
                                    <option value="">Selecione um funcionário</option>
                                    {funcionarios.map((funcionario) => (
                                        <option key={funcionario.id} value={funcionario.id}>
                                            {`${funcionario.nome} ${funcionario.sobrenome}`}
                                        </option>
                                    ))}
                                </select>
                                <button
                                    type="button"
                                    className="btn btn-dark ms-2"
                                    onClick={handleAddFuncionario}
                                >
                                    <FontAwesomeIcon icon={faPlus} />
                                </button>
                            </div>
                        </div>
                        <h6>Funcionários selecionados</h6>
                        <ul className="list-group">
                            {funcionariosVinculados.map((funcionario) => (
                                <li key={funcionario.id} className="list-group-item d-flex justify-content-between">
                                    {`${funcionario.nome} ${funcionario.sobrenome}`}
                                    <button
                                        type="button"
                                        className="btn btn-danger btn-sm"
                                        onClick={() => handleRemoveFuncionario(funcionario.id)}
                                    >
                                        Remover
                                    </button>
                                </li>
                            ))}
                        </ul>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" onClick={onClose}>
                            Cancelar
                        </button>
                        <button type="button" className="btn btn-dark" onClick={handleVincularFuncionarios}>
                            Vincular
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default VincularFuncionarios;
