import axios from 'axios';
import { AuthService } from './auth.service';

jest.mock('axios');
const mockedAxios = axios as jest.Mocked<typeof axios>;

describe('Test login', () => {
    const username: string = 'user';
    const password: string = 'password';

    describe('with success', () => {
        const data = {
            token: 'abcdef',
            userInformation: { username: username, email: 'email@example.com', displayName: 'User' },
            expires: new Date()
        };

        beforeEach(() => {
            mockedAxios.post.mockResolvedValue({ data: data });
        });

        it('should call endpoint with given email and password', async () => {
            await AuthService.login({ username, password });
            expect(axios.post).toBeCalledWith(
                `${process.env.REACT_APP_BASE_API_URL}/auth/login`,
                { username, password }
            );
        });
    });

    // describe('with failed', () => {

    // });
});

// describe('Test registration', () => {
//     it('registration success', () => {

//     });

//     it('registration failed', () => {

//     });
// });
