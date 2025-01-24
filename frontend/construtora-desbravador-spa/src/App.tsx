import './App.css';
import Menu from './components/Menu';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Funcionarios from './components/Funcionarios';
import Projetos from './components/Projetos';
import Home from './components/Home';

function App() {
  return (
    <Router>
        <Menu />
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/funcionarios" element={<Funcionarios />} />
            <Route path="/projetos" element={<Projetos />} />
      </Routes>
    </Router>
  );
}

export default App;
