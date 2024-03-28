FROM golang:alpine AS ms-tours-builder
WORKDIR /app
COPY go.mod .
COPY go.sum .
RUN go mod download
COPY . .
RUN go build -o ms-tours-webapp

FROM alpine
COPY --from=ms-tours-builder /app/ms-tours-webapp /usr/bin/ms-tours-webapp
EXPOSE 8081
ENTRYPOINT ["/usr/bin/ms-tours-webapp"]
