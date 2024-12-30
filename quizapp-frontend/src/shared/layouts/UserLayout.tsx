import Footer from "../components/Footer"
import Header from "../components/Header"
import Sidebar from "../components/Sidebar"

const UserLayout = ({ children }: { children: React.ReactNode }) => {
    return (
        <div className="flex flex-col h-screen justify-between">
            <Header />
            <main className="mb-auto">
                { children }
            </main>
            <Footer />
        </div>
    );
}

export default UserLayout;