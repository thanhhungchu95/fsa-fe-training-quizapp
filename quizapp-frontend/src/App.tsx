import { BrowserRouter, Routes, Route } from "react-router-dom";
import About from "./pages/static/About";
import Home from "./pages/static/Home";
import ManagerLayout from "./shared/layouts/ManagerLayout";
import UserLayout from "./shared/layouts/UserLayout";
import Contact from "./pages/static/Contact";
import AdminDashboard from "./pages/management/AdminDashboard";

function App() {
  return (
    <BrowserRouter>
      <div className="main-content min-h-screen flex flex-col">
        <Routes>
          {/* Customer Router */}
          <Route path="/" element={<UserLayout><Home /></UserLayout>} />
          <Route path="/about" element={<UserLayout><About /></UserLayout>} />
          <Route path="/contact" element={<UserLayout><Contact /></UserLayout>} />

          {/* Admin Router */}
          <Route path="/management/dashboard" element={<ManagerLayout><AdminDashboard /></ManagerLayout>} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
