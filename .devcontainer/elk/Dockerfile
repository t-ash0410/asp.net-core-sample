FROM sebp/elk:latest
ENV PATH $PATH:/opt/elasticsearch/bin
RUN elasticsearch-plugin install analysis-kuromoji
COPY ./elk/logstash.conf /etc/logstash/conf.d/logstash.conf
