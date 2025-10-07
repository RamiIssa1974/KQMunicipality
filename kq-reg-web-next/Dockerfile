# ---- deps ----
FROM node:20-alpine AS deps
WORKDIR /app
COPY package*.json ./
# Use npm ci if lockfile exists; falls back to npm install
RUN if [ -f package-lock.json ]; then npm ci; else npm install; fi

# ---- builder ----
FROM node:20-alpine AS builder
WORKDIR /app

ARG NEXT_PUBLIC_API_BASE
ENV NEXT_PUBLIC_API_BASE=$NEXT_PUBLIC_API_BASE
COPY --from=deps /app/node_modules ./node_modules
COPY . .
# If you use NEXT_PUBLIC_* envs, they’re read at build time; we'll inject via compose if needed
RUN npm run build

# ---- runner ----
FROM node:20-alpine AS runner
WORKDIR /app
ENV NODE_ENV=production
# Install only production deps for runtime
COPY package*.json ./
RUN if [ -f package-lock.json ]; then npm ci --omit=dev; else npm install --omit=dev; fi

# Copy build output
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/public ./public
COPY --from=builder /app/package.json ./package.json

EXPOSE 3000
CMD ["npm","run","start","--","-p","3000"]
