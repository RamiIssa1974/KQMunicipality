import type { NextConfig } from "next";

const nextConfig = {
    async rewrites() {
        return [
            {
                source: '/api/:path*',
                destination: 'http://kq-api:8080/api/:path*', // inside Docker network
            },
        ];
    },
};

export default nextConfig;
