FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /var/app
ADD publish/ /var/app/

RUN apt-get update && apt-get install -y build-essential libpthread-stubs0-dev libssl-dev ca-certificates libasound2 wget

RUN wget -O - https://www.openssl.org/source/openssl-1.1.1u.tar.gz | tar zxf -  \
    && cd openssl-1.1.1u  \
    && ./config --prefix=/usr/local  \
    && make -j $(nproc)  \
    && make install_sw install_ssldirs  \
    && ldconfig -v  \
    && export SSL_CERT_DIR=/etc/ssl/certs

RUN export LD_LIBRARY_PATH=/usr/local/lib:$LD_LIBRARY_PATH
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "CogDockerTest.dll"]
