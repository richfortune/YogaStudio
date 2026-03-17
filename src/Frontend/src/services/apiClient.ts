const API_BASE_URL = import.meta.env.VITE_API_URL || '/api';

/**
 * Generic fetch wrapper for API calls that automatically parses JSON 
 * and handles common HTTP errors.
 */
async function fetchClient<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
    const url = `${API_BASE_URL}${endpoint}`;
    console.log(`[API Client] Calling: ${url}`);
    
    const headers = {
        'Content-Type': 'application/json',
        ...options.headers,
    };

    try {
        const response = await fetch(url, { ...options, headers });

        if (!response.ok) {
            const errorText = await response.text();
            console.error(`[API Client] Error ${response.status}: ${errorText}`);
            throw new Error(`API Error ${response.status}: ${errorText || response.statusText}`);
        }

        if (response.status === 204) {
            return {} as T;
        }

        return response.json();
    } catch (error) {
        console.error(`[API Client] Network Error:`, error);
        throw error;
    }
}

/**
 * Service modules exported for easy consumption across components.
 */
export const ApiHelper = {
    get: <T>(url: string) => fetchClient<T>(url),
    post: <T>(url: string, body: any) =>
        fetchClient<T>(url, { method: 'POST', body: JSON.stringify(body) }),
    put: <T>(url: string, body: any) =>
        fetchClient<T>(url, { method: 'PUT', body: JSON.stringify(body) }),
    delete: <T>(url: string) => fetchClient<T>(url, { method: 'DELETE' }),
};
