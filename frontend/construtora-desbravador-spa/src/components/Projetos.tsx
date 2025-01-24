import React, { useEffect, useState } from 'react';
import { fetchProjetos, excluirProjeto, atualizarProjeto, inserirProjeto } from '../services/projetosService';
import { Projeto } from '../interfaces/Projeto';
import { StatusProjetoEnumDescription } from '../enums/StatusProjetoEnum';
import { RiscoProjetoEnumDescription } from '../enums/RiscoProjetoEnum';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEye, faTrash, faEdit, faUsers } from '@fortawesome/free-solid-svg-icons';
import ProjetoModal from './Modal/ProjetoModal';
import ProjetoInsertModal from './Modal/ProjetoInsertEditModal';
import { ProjetoInsert } from '../interfaces/ProjetoInsert';
import VincularFuncionarios from './Modal/VincularFuncionarios';

const Projetos: React.FC = () => {
    const [projetos, setProjetos] = useState<Projeto[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [totalPages, setTotalPages] = useState<number>(0);
    const [pageSize, setPageSize] = useState<number>(10); 
    const [selectedProjeto, setSelectedProjeto] = useState<Projeto | null>(null);     
    const [showModal, setShowModal] = useState(false); 
    const [showInsertModal, setShowInsertModal] = useState(false);
    const [projetoEditando, setProjetoEditando] = useState<ProjetoInsert | null>(null);
    const [showVincularModal, setShowVincularModal] = useState(false);
    const [projetoSelecionado, setProjetoSelecionado] = useState<Projeto | null>(null);

    const handleOpenInsertModal = () => {
        setProjetoEditando(null); 
        setShowInsertModal(true);
    };

    const handleOpenEditModal = (projeto: ProjetoInsert) => {
        setProjetoEditando(projeto); 
        setShowInsertModal(true);
    };

    const handleSaveProjeto = async (projeto: ProjetoInsert) => {
        if (projeto.id === 0) {

            const payload = {                
                ...projeto,
                statusProjeto: parseInt(projeto.statusProjeto.toString()),
                riscoProjeto: parseInt(projeto.riscoProjeto.toString()),           
            };

            const data = await inserirProjeto(payload); // Função do service
            getProjetos();
        } else {
            const data = await atualizarProjeto(projeto.id, projeto); // Função do service
            getProjetos();
        }
        setShowInsertModal(false); 
    };
    const getProjetos = async () => {
        try {
            const data = await fetchProjetos(currentPage, pageSize);
            setProjetos(data.items); 
            setTotalPages(data.totalPages); 
        } catch (error) {
            console.error('Erro ao carregar projetos:', error);
        } finally {
            setLoading(false);
        }
    };
    
    useEffect(() => {
        getProjetos();
    }, [currentPage, pageSize]);

    const handlePageChange = (page: number) => {
        setCurrentPage(page);
    };

    const handleVisualizarDetalhes = (projeto: Projeto) => {
        setSelectedProjeto(projeto); 
        const modal = new (window as any).bootstrap.Modal(document.getElementById('modalDetalhes')!); 
        modal.show();        
        setShowModal(true); 
    };

    const handleCloseModal = () => {
        setShowModal(false);         
        setSelectedProjeto(null); 
    };

    const handleExcluirProjeto = async (id: number) => {
        const confirmacao = window.confirm('Tem certeza que deseja excluir este funcionário?');
        if (confirmacao) {
            await excluirProjeto(id);    
            setProjetos(projetos.filter(projeto => projeto.id !== id));
        }
    }

    const handleCloseModalInsert = () => {
        setShowInsertModal(false)
        setProjetoEditando(null);
        
    };

    const handleVincularFuncionarios = (projeto: Projeto) => {
        setProjetoSelecionado(projeto);
        setShowVincularModal(true);
    };

    return (
        <div className="container mt-5">
            <h1>Projetos</h1>
            <button className="btn btn-dark mb-4" onClick={handleOpenInsertModal}>
                Inserir
            </button>
            <ProjetoInsertModal
                showModal={showInsertModal}
                onClose={handleCloseModalInsert}
                onSave={handleSaveProjeto}
                projeto={projetoEditando}
            />

            <VincularFuncionarios
                projetoId={projetoSelecionado?.id || 0}
                showModal={showVincularModal}
                onClose={() => setShowVincularModal(false)}
                onFuncionariosVinculados={getProjetos}
            />

            {loading ? (
                <p>Carregando...</p>
            ) : (
                <>
                    <table className="table table-striped">
                        <thead>
                            <tr>
                                <th>Nome</th>
                                <th>Status</th>
                                <th>Risco</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            {projetos.map((projeto) => (
                                <tr key={projeto.id}>
                                    <td>{projeto.nome}</td>
                                    <td>{StatusProjetoEnumDescription[projeto.statusProjeto]}</td>
                                    <td>{RiscoProjetoEnumDescription[projeto.riscoProjeto]}</td>               
                                    <td>
                                        <button className="btn btn-dark btn-sm me-2" onClick={() => handleVisualizarDetalhes(projeto)}
                                            title="Visualizar Detalhes">
                                            <FontAwesomeIcon icon={faEye} />
                                        </button>
                                        <button className="btn btn-dark btn-sm me-2" onClick={() => handleVincularFuncionarios(projeto)}
                                            title="Vincular Funcionários">
                                            <FontAwesomeIcon icon={faUsers} />
                                        </button>
                                        <button className="btn btn-dark btn-sm me-2" title="Editar Funcionário"
                                            onClick={() => handleOpenEditModal({id: projeto.id,
                                                nome: projeto.nome,
                                                descricao: projeto.descricao,
                                                dataInicio: projeto.dataInicio,
                                                dataTermino: projeto.dataTermino,
                                                statusProjeto: projeto.statusProjeto,
                                                riscoProjeto: projeto.riscoProjeto,
                                                responsavelId: projeto.responsavel.id
                                            })}>
                                            <FontAwesomeIcon icon={faEdit} />
                                        </button>
                                        <button className="btn btn-danger btn-sm" onClick={() => handleExcluirProjeto(projeto.id)}
                                            title="Excluir Funcionário">
                                            <FontAwesomeIcon icon={faTrash} />
                                        </button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>

                    {/* Paginação */}
                    <div className="mt-3">
                        <nav>
                            <ul className="pagination justify-content-center">
                                <li className={`page-item ${currentPage === 1 ? 'disabled' : ''}`}>
                                    <button
                                        className="page-link"
                                        onClick={() => handlePageChange(currentPage - 1)}
                                        disabled={currentPage === 1}
                                    >
                                        Anterior
                                    </button>
                                </li>
                                {Array.from({ length: totalPages }, (_, index) => (
                                    <li
                                        key={index + 1}
                                        className={`page-item ${currentPage === index + 1 ? 'active' : ''}`}
                                    >
                                        <button
                                            className="page-link"
                                            onClick={() => handlePageChange(index + 1)}
                                        >
                                            {index + 1}
                                        </button>
                                    </li>
                                ))}
                                <li
                                    className={`page-item ${currentPage === totalPages ? 'disabled' : ''}`}
                                >
                                    <button
                                        className="page-link"
                                        onClick={() => handlePageChange(currentPage + 1)}
                                        disabled={currentPage === totalPages}
                                    >
                                        Próxima
                                    </button>
                                </li>
                            </ul>
                        </nav>
                    </div>
                </>
            )}
            {/* Modal de Detalhes do Projeto */}
            <ProjetoModal
                projeto={selectedProjeto}
                showModal={showModal}
                onClose={handleCloseModal} // Passa a função para fechar o modal
            />
        </div>
    );
};

export default Projetos;
