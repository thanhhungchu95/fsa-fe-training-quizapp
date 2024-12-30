import quizappbg from '../../assets/pictures/quiz-authen-bg.svg';

const AuthenticationLayout = ({ children }: { children: React.ReactNode }) => {
    return (
        <main className="flex-1 relative h-screen">
            <img src={quizappbg} alt={"Quiz Application"} className="w-full h-screen object-cover" />
            <div className="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2">
                {children}
            </div>
        </main>
    );
}

export default AuthenticationLayout;