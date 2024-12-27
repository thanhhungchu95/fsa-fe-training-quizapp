import Footer from "../components/Footer"
import Header from "../components/Header"
import Sidebar from "../components/Sidebar"

const UserLayout = ({ children }: { children: React.ReactNode }) => {
    return (
        <>
            <Header />
            <main>
                <Sidebar />
                { children }
            </main>
            <Footer />
        </>
    );
}

export default UserLayout;