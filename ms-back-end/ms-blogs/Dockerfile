FROM golang:alpine AS ms-blogs-builder
WORKDIR /app
COPY go.mod .
COPY go.sum .
RUN go mod download
COPY . .
RUN go build -o ms-blogs-webapp

FROM alpine
COPY --from=ms-blogs-builder /app/ms-blogs-webapp /usr/bin/ms-blogs-webapp
EXPOSE 8080
ENTRYPOINT ["/usr/bin/ms-blogs-webapp"]
