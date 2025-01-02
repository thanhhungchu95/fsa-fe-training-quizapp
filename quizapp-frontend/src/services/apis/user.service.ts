import { AxiosInstance } from "axios";
import { BaseApiService } from "./base.service";

const api: AxiosInstance = BaseApiService.createApiServiceInstance('users');
