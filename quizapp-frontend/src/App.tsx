import { BrowserRouter, Routes, Route } from "react-router-dom";
import About from "./pages/About";
import Home from "./pages/Home";
import ManagementLayout from "./shared/layouts/ManagementLayout";
import UserLayout from "./shared/layouts/UserLayout";
import Contact from "./pages/Contact";
import AdminDashboard from "./pages/management/AdminDashboard";
import QuizList from "./pages/management/quizzes/QuizList";
import QuestionList from "./pages/management/questions/QuestionList";
import UserList from "./pages/management/users/UserList";
import RoleList from "./pages/management/roles/RoleList";
import Login from "./pages/authenticate/Login";
import AuthenticationLayout from "./shared/layouts/AuthenticationLayout";
import Register from "./pages/authenticate/Register";
import Quizzes from "./pages/Quizzes";

function App() {
  return (
    <BrowserRouter>
      <div className="main-content min-h-screen flex flex-col">
        <Routes>
          {/* User Router */}
          <Route path="/" element={<UserLayout><Home /></UserLayout>} />
          <Route path="/quizzes" element={<UserLayout><Quizzes /></UserLayout>} />
          <Route path="/about" element={<UserLayout><About /></UserLayout>} />
          <Route path="/contact" element={<UserLayout><Contact /></UserLayout>} />

          {/* Admin Router */}
          <Route path="/management/dashboard" element={<ManagementLayout><AdminDashboard /></ManagementLayout>} />
          <Route path="/management/quiz" element={<ManagementLayout><QuizList /></ManagementLayout>} />
          <Route path="/management/question" element={<ManagementLayout><QuestionList /></ManagementLayout>} />
          <Route path="/management/user" element={<ManagementLayout><UserList /></ManagementLayout>} />
          <Route path="/management/role" element={<ManagementLayout><RoleList /></ManagementLayout>} />

          {/* Anonymous Router */}
          <Route path="/auth/login" element={<AuthenticationLayout><Login /></AuthenticationLayout>} />
          <Route path="/auth/register" element={<AuthenticationLayout><Register /></AuthenticationLayout>} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
