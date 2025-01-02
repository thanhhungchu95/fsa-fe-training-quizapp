const Loading = () => {
    return (
        <div className="top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 absolute">
            <div className="w-16 h-16 rounded-full absolute border-16 border-solid border-gray-200">
            </div>
            <div className="w-16 h-16 rounded-full animate-spin-slower absolute border-16 border-solid border-green-600 border-t-transparent">
            </div>
        </div>
    );
}

export default Loading;