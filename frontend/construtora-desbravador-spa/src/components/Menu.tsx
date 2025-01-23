import React from 'react';
import { Link } from 'react-router-dom';

const Menu: React.FC = () => {
  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      {/* Container para o conteúdo da Navbar */}
      <div className="container-fluid">
        {/* Logo ou título, pode ser um Link */}
        <Link className="navbar-brand" to="/">Construtora Desbravador</Link>

        {/* Botão de menu (aparece em telas pequenas) */}
        <button 
          className="navbar-toggler" 
          type="button" 
          data-bs-toggle="collapse" 
          data-bs-target="#navbarNav" 
          aria-controls="navbarNav" 
          aria-expanded="false" 
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        {/* Links de navegação */}
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav">
            {/* Link Home */}
            <li className="nav-item">
              <Link className="nav-link active" to="/">Home</Link>
            </li>

            {/* Link Sobre */}
            <li className="nav-item">
              <Link className="nav-link" to="/projetos">Projetos</Link>
            </li>

            {/* Link para algum outro item */}
            <li className="nav-item">
              <Link className="nav-link" to="/funcionarios">Funcionários</Link>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Menu;