import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import Navbar from "./components/navbar";
import { Login, Register } from "./pages";

function App() {
  return (
    <main className="bg-slate-300/20 min-h-screen">
      <Router>
        <Navbar />
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/register" element={<Register />} />
        </Routes>
      </Router>
    </main>
  );
}

export default App;
