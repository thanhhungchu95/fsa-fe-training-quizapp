import { render, screen } from '@testing-library/react';
import Home from './Home';

describe('Test home page component', () => {
    it('should render correctly', () => {
        render(<Home/>);
        const heading = screen.getByText(/Welcome to Quiz App/i);
        expect(heading).toBeInTheDocument();
    });
});
