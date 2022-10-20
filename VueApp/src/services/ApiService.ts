async function http<T>(path: string, config: RequestInit): Promise<T> {
const request = new Request(path, config)
const response = await fetch(request)

return response.json()
}

export async function get<T>(path: string, config?: RequestInit): Promise<T> {
	const init = {method: 'get', ...config}
	return await http<T>(path, init)
}

export async function post<T, U>(path: string, body: T, config?: RequestInit): Promise<U> {
	const headers: { [key: string]: string } = { 'Content-Type' : 'application/json' };
	const init = {method: 'post', headers: headers, body: JSON.stringify(body), ...config}
	return await http<U>(path, init)
}

export async function put<T, U>(path: string, body: T, config?: RequestInit): Promise<U> {
	const headers: { [key: string]: string } = { 'Content-Type' : 'application/json' };
	const init = {method: 'put', headers: headers, body: JSON.stringify(body), ...config}
	return await http<U>(path, init)
}

export async function del<T>(path: string, config?: RequestInit): Promise<T> {
	const init = {method: 'DELETE', ...config}
	return await http<T>(path, init)
}