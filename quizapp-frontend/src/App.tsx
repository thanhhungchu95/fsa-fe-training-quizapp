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
import { ToastContainer } from "react-toastify";
import { Provider } from "react-redux";
import store from "./stores/store";
import AnonymousRoute from "./shared/components/routers/AnonymousRoute";
import ForbiddenPage from "./shared/components/static/error-handling/ForbiddenPage";
import AnonymousLayout from "./shared/layouts/AnonymousLayout";
import NotFoundPage from "./shared/components/static/error-handling/NotFoundPage";
import AuthenticatedRoute from "./shared/components/routers/AuthenticatedRoute";

function App() {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <div className="main-content min-h-screen flex flex-col">
          <Routes>
            {/* User Router */}
            <Route path="/" element={<UserLayout><Home /></UserLayout>} />
            <Route path="/quizzes" element={<UserLayout><Quizzes /></UserLayout>} />
            <Route path="/about" element={<UserLayout><About /></UserLayout>} />
            <Route path="/contact" element={<UserLayout><Contact /></UserLayout>} />

            <Route element={<AuthenticatedRoute roles={["Admin", "Editor"]} />}>
              {/* Admin Router */}
              <Route path="/management/dashboard" element={<ManagementLayout><AdminDashboard /></ManagementLayout>} />
              <Route path="/management/quiz" element={<ManagementLayout><QuizList /></ManagementLayout>} />
              <Route path="/management/question" element={<ManagementLayout><QuestionList /></ManagementLayout>} />
              <Route path="/management/user" element={<ManagementLayout><UserList /></ManagementLayout>} />
              <Route path="/management/role" element={<ManagementLayout><RoleList /></ManagementLayout>} />

              {/* Error Router */}
              <Route path="/error/forbidden" element={<AnonymousLayout><ForbiddenPage /></AnonymousLayout>} />
              <Route path="/error/notfound" element={<AnonymousLayout><NotFoundPage /></AnonymousLayout>} />
            </Route>

            <Route element={<AnonymousRoute />}>
              {/* Authentication Router */}
              <Route path="/auth/login" element={<AuthenticationLayout><Login /></AuthenticationLayout>} />
              <Route path="/auth/register" element={<AuthenticationLayout><Register /></AuthenticationLayout>} />
            </Route>     
          </Routes>
        </div>
      </BrowserRouter>
      <ToastContainer className="mt-12" position="top-right" autoClose={3000} newestOnTop={true} closeOnClick={false} draggable={false} pauseOnHover={true} />
    </Provider>
  );
}

export default App;
