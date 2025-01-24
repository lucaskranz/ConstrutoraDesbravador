import React, { useEffect, useState } from 'react';
import { faEye, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Funcionario } from '../interfaces/Funcionario';
import { getFuncionarios, inserirAleatorios } from '../services/funcionariosService';
import { PaginacaoResult } from '../interfaces/PaginacaoResult';
import FuncionarioModal from './Modal/FuncionarioModal';

const FuncionariosGrid: React.FC = () => {
    const [funcionarios, setFuncionarios] = useState<Funcionario[]>([]);
    const [total, setTotal] = useState(0);
    const [page, setPage] = useState(1);
    const [size] = useState(10);
    const [totalPages, setTotalPages] = useState(0);
    const [selectedFuncionario, setSelectedFuncionario] = useState<Funcionario | null>(null); 
    const [showModal, setShowModal] = useState(false); 



    // Função para carregar os funcionários
    const loadFuncionarios = async () => {
        try {
        const data: PaginacaoResult<Funcionario> = await getFuncionarios(page, size);
        setFuncionarios(data.items); // Atualiza os funcionários
        setTotal(data.total); // Atualiza o total de funcionários
        setTotalPages(data.totalPages); // Atualiza o número total de páginas
        } catch (error) {
        console.error("Erro ao carregar funcionários", error);
        }
    };

    // Carregar os funcionários ao montar o componente ou mudar de página
    useEffect(() => {
        loadFuncionarios();
    }, [page, size]);

    // Função para inserir funcionários aleatórios
    const handleInserirAleatorios = async () => {
        try {
            await inserirAleatorios(); // Chama o serviço para inserir funcionários aleatórios
            loadFuncionarios(); // Recarrega a lista de funcionários após a inserção
        } catch (error) {
            console.error('Erro ao inserir aleatórios', error);
        }
    };

    // Função para mudar de página
    const handlePageChange = (newPage: number) => {
        if (newPage > 0 && newPage <= totalPages) {
            setPage(newPage);
        }
    };
    
    const handleVisualizarDetalhes = (funcionario: Funcionario) => {
        setSelectedFuncionario(funcionario); // Armazena o funcionário selecionado
        const modal = new (window as any).bootstrap.Modal(document.getElementById('modalDetalhes')!); // Inicializa o modal
        modal.show();        
        setShowModal(true); 
    };

    const handleCloseModal = () => {
        setShowModal(false);         
        setSelectedFuncionario(null); 
      };

    return (
        <div className="container mt-3">
            <h1>Funcionários</h1>

            {/* Botão para Inserir Aleatórios */}
            <button className="btn btn-dark mb-3" onClick={handleInserirAleatorios}>
                Inserir Aleatórios
            </button>

            {/* Tabela de Funcionários */}
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Email</th>
                        <th>Ações</th>
                    </tr>
                </thead>
                <tbody>
                {funcionarios.map((funcionario) => (
                    <tr key={funcionario.id}>
                        <td>{funcionario.nome}</td>
                        <td>{funcionario.email}</td>
                        <td>
                            <button className="btn btn-info btn-sm me-2" onClick={() => handleVisualizarDetalhes(funcionario)}>
                                <FontAwesomeIcon icon={faEye} />
                            </button>
                            <button className="btn btn-danger btn-sm">
                                <FontAwesomeIcon icon={faTrash} />
                            </button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>

            {/* Paginação */}
            <nav>
                <ul className="pagination justify-content-end">
                    <li className={`page-item ${page === 1 ? 'disabled' : ''}`}>
                    <button className="page-link btn-dark" onClick={() => handlePageChange(page - 1)}>Anterior</button>
                    </li>
                    {[...Array(totalPages)].map((_, index) => (
                    <li
                        key={index}
                        className={`page-item ${page === index + 1 ? 'active' : ''}`}
                    >
                        <button className="page-link btn-dark" onClick={() => handlePageChange(index + 1)}>
                        {index + 1}
                        </button>
                    </li>
                    ))}
                    <li className={`page-item ${page === totalPages ? 'disabled' : ''}`}>
                    <button className="page-link btn-dark" onClick={() => handlePageChange(page + 1)}>Próxima</button>
                    </li>
                </ul>
            </nav>

            {/* Modal de Detalhes do Funcionário */}
            <FuncionarioModal
                funcionario={selectedFuncionario}
                showModal={showModal}
                onClose={handleCloseModal} // Passa a função para fechar o modal
            />
        </div>
    );
};

export default FuncionariosGrid;