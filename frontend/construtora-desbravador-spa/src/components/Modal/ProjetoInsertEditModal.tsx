import React, { useState, useEffect } from 'react';
import { Projeto } from '../../interfaces/Projeto';
import { ProjetoInsert } from '../../interfaces/ProjetoInsert';
import { StatusProjetoEnum, StatusProjetoEnumDescription } from '../../enums/StatusProjetoEnum';
import { RiscoProjetoEnum, RiscoProjetoEnumDescription } from '../../enums/RiscoProjetoEnum';
import axios from 'axios';
import { API_URL } from '../../config/config';
import { Funcionario } from '../../interfaces/Funcionario';
import { getFuncionarios } from '../../services/funcionariosService';

interface ProjetoInsertModalProps {
    showModal: boolean;
    onClose: () => void;
    onSave: (projeto: ProjetoInsert) => void;
    projeto?: ProjetoInsert | null; // Projeto existente para edição, ou null para inserção
}

const ProjetoInsertModal: React.FC<ProjetoInsertModalProps> = ({ showModal, onClose, onSave, projeto }) => {
    const [projetoState, setProjetoState] = useState<ProjetoInsert>({
        id: 0,
        nome: '',
        descricao: '',
        dataInicio: new Date(),
        dataTermino: new Date(),
        statusProjeto: StatusProjetoEnum.EmAnalise, // Default
        riscoProjeto: RiscoProjetoEnum.Baixo, // Default
        responsavelId: 0,
    });
    const [funcionarios, setFuncionarios] = useState<Funcionario[]>([]);

    // Atualiza o estado inicial com os valores do projeto quando em edição
    useEffect(() => {
        if (projeto) {
            setProjetoState(projeto);
        }
        fetchFuncionarios();
        
    }, [projeto]);
    
    const fetchFuncionarios = async () => {
        try {
            const response = await getFuncionarios(1, 1000);
            setFuncionarios(response.items);
        } catch (error) {
            console.error('Erro ao carregar funcionários:', error);
            alert('Não foi possível carregar os funcionários.');
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setProjetoState((prev: any) => ({
            ...prev,
            [name]: name === 'responsavelId' ? parseInt(value) : value, // Converte responsavelId para número
        }));
    };

    const handleSubmit = () => {
        onSave(projetoState); // Envia o projeto (novo ou editado) para a função do pai
        handleClose(); // Fecha o modal        
    };

    const handleClose = () => {
        onClose(); // Fecha o modal
        setProjetoState({
            id: 0,
            nome: '',
            descricao: '',
            dataInicio: new Date(),
            dataTermino: new Date(),
            statusProjeto: StatusProjetoEnum.EmAnalise,
            riscoProjeto: RiscoProjetoEnum.Baixo,
            responsavelId: 0,
        });
    }

    return (
        <div className={`modal ${showModal ? 'show' : ''}`} style={{ display: showModal ? 'block' : 'none' }}>
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">
                            {projeto ? 'Editar Projeto' : 'Inserir Novo Projeto'}
                        </h5>
                        <button type="button" className="btn-close" onClick={handleClose}></button>
                    </div>
                    <div className="modal-body">
                        <form>
                            <div className="mb-3">
                                <label className="form-label">Nome</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    name="nome"
                                    value={projetoState.nome}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Descrição</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    name="descricao"
                                    value={projetoState.descricao}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Data de Início</label>
                                <input
                                    type="date"
                                    className="form-control"
                                    name="dataInicio"
                                    value={new Date(projetoState.dataInicio).toISOString().split('T')[0]}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Data de Término</label>
                                <input
                                    type="date"
                                    className="form-control"
                                    name="dataTermino"
                                    value={new Date(projetoState.dataTermino).toISOString().split('T')[0]}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Status</label>
                                <select
                                    className="form-select"
                                    name="statusProjeto"
                                    value={projetoState.statusProjeto}
                                    onChange={handleChange}
                                >
                                    {Object.keys(StatusProjetoEnum)
                                        .filter((key) => isNaN(Number(key))) 
                                        .map((key) => (
                                            <option key={key} value={StatusProjetoEnum[key as keyof typeof StatusProjetoEnum] as number}>
                                                {StatusProjetoEnumDescription[StatusProjetoEnum[key as keyof typeof StatusProjetoEnum]]}
                                            </option>
                                        ))}
                                </select>
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Risco</label>
                                <select
                                    className="form-select"
                                    name="riscoProjeto"
                                    value={projetoState.riscoProjeto}
                                    onChange={handleChange}
                                >
                                    {Object.keys(RiscoProjetoEnum)
                                        .filter((key) => isNaN(Number(key))) // Filtra as chaves que não são números (caso seja um enum numérico)
                                        .map((key) => (
                                            <option key={key} value={RiscoProjetoEnum[key as keyof typeof RiscoProjetoEnum] as number}>
                                                {RiscoProjetoEnumDescription[RiscoProjetoEnum[key as keyof typeof RiscoProjetoEnum]]}
                                            </option>
                                        ))}
                                </select>
                            </div>
                            <div className="mb-3">
                                <label htmlFor="responsavelId" className="form-label">
                                    Responsável
                                </label>
                                <select
                                    id="responsavelId"
                                    className="form-select"
                                    value={projetoState.responsavelId || ''}
                                    onChange={(e) => setProjetoState({ ...projetoState, responsavelId: parseInt(e.target.value) })}
                                >
                                    <option value="">Selecione um responsável</option>
                                    {funcionarios.map((funcionario) => (
                                        <option key={funcionario.id} value={funcionario.id}>
                                            {`${funcionario.nome} ${funcionario.sobrenome}`}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </form>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" onClick={handleClose}>
                            Cancelar
                        </button>
                        <button type="button" className="btn btn-primary" onClick={handleSubmit}>
                            {projeto ? 'Salvar Alterações' : 'Cadastrar'}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ProjetoInsertModal;