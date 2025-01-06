const AnonymousLayout = ({ children }: { children: React.ReactNode }) => {
    return (
        <main className="flex-1 relative h-screen">
            {children}
        </main>
    );
}

export default AnonymousLayout;