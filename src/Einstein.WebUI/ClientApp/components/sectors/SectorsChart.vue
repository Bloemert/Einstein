<!--
Links
  Data: https://vuejs.org/v2/guide/components.html#data-Must-Be-a-Function
  Form Input Bindings: https://vuejs.org/v2/guide/forms.html#Basic-Usage
  Watchers: https://vuejs.org/v2/guide/computed.html#Watchers
-->

<template>
  <div>
    <svg width="500" height="300"></svg>
    <div class="slider"></div>
  </div>
</template>

<script>
  import { mapSectorsActions, mapSectorsFields, mapSectorsMultiRowFields } from './store';

  const d3 = require('d3');

  export default {
    data: function () {
      return {
        width: 500,
        height: 300,

        circleSize: function () {
          return (this.height / 2) - 10;
        },

        svg: {},
        mainGraph: {},
        zoom: {},
        slider: {}
      }
    },

    methods: {
      ...mapSectorsActions({
        loadData: 'loadFromDB',
      }),

    },

    computed: {

      ...mapSectorsFields({
        sectors: 'rows',
        selectedSeqno: 'selectedSeqno',
        currentPage: 'currentPage'
      })

    },

    mounted: function () {
      this.loadData()
        .then((r) => {
          var that = this;

          var centerX = this.width / 2;
          var centerY = this.height / 2;

          var div = d3.select("body").append("div")
            .attr("class", "tooltip")
            .style("opacity", 0);

          this.svg = d3.select(this.$el).select('svg');
          this.mainGraph = this.svg.append('g')
            .attr("transform", 'scale(1)')
            .style("transform-origin", "50% 50% 0");

          this.zoom = d3.zoom()
            .scaleExtent([1, 10])
            .on("zoom", function () {
              that.mainGraph
                .attr("transform", 'scale(' + d3.event.transform.k + ')')
              that.slider.property("value", d3.event.scale);
            });

          this.slider = d3.select("div.slider").append("input")
            .datum({})
            .attr("type", "range")
            .attr("value", this.zoom.scaleExtent()[0])
            .attr("min", this.zoom.scaleExtent()[0])
            .attr("max", this.zoom.scaleExtent()[1])
            .attr("step", (this.zoom.scaleExtent()[1] - that.zoom.scaleExtent()[0]) / 100)
            .on("input", function (d) {
              that.mainGraph.transition()
                .duration(10)
                .call(that.zoom.transform,
                  d3.zoomIdentity
                    .scale(this.value)
                );
            });

          this.outerCircle = this.mainGraph
            .append('circle')
            .attr('cx', centerX)
            .attr('cy', centerY)
            .attr('r', that.circleSize())
            .attr('class', 'outer-ring');


          var mySectors = _cloneDeep(this.sectors);

          var pieGenerator = d3.pie();
          var data = [];
          for (var i = 1; i <= mySectors.length; i++) {
            data.push(1)
          }
          var arcData = pieGenerator(data);

          var mySectorsZipped = d3.zip(mySectors, arcData);


          var arcGenerator = d3.arc()
            .innerRadius(25)
            .outerRadius(that.circleSize())
            .cornerRadius(5);

          var labelArc = d3.arc()
            .outerRadius(that.circleSize() - 40)
            .innerRadius(that.circleSize() - 40)

          var pieG = this.mainGraph
            .append('g')
            .attr("transform", 'translate(' + centerX + ', ' + centerY + ')');

          var piePath = pieG.selectAll('path')
            .data(mySectorsZipped)
            .enter()
            .append('path')
            .attr('d', function (d) {
              return arcGenerator(d[1]);
            })
            .attr('class', 'pie')
            .on('mouseover', function (d) {
              d3.select(this)
                .attr('class', 'pie hover-over');
            })
            .on('mouseout', function (d) {
              d3.select(this)
                .attr('class', 'pie');
            });

          pieG.selectAll('text')
            .data(mySectorsZipped)
            .enter()
            .append('text')
            .each(function (d) {
              var centroid = arcGenerator.centroid(d[1]);
              d3.select(this)
                .attr('x', centroid[0])
                .attr('y', centroid[1])
                .attr('dy', '0.33em')
                .text(d[0].id);
            });

        });
    },

    watch: {
      selectedSeqno: {
        handler: function (newVal, oldVal) {

        }
      }
    },

  }
</script>

<style lang="scss">
  @import '../../styles/site.scss';

  .outer-ring {
    fill: none;
    stroke: #000;
    stroke-width: 4;
    transform-origin: 50% 50% 0;
  }

  .inner-sector {
    fill: none;
    stroke: #808080;
    stroke-width: 2;
  }

  .inner-sector-hover {
    fill: none;
    stroke: white;
  }

  .inner-sector-hover-over {
    fill: none;
    stroke: $primary;
  }

  .pie-label {
    fill: black;
    text-anchor: middle;
  }

  .pie-label-hover {
    font-size: 24pt;
    color: black;
    fill: black;
  }

  div.tooltip {
    position: absolute;
    text-align: center;
    width: 75px;
    height: 28px;
    padding: 2px;
    font: 12px sans-serif;
    background: $primary;
    color: $primary-invert;
    border-width: 1px;
    border-color: black;
    border-style: solid;
    border-radius: 8px;
    pointer-events: none;
  }

  .pie {
    fill: white;
    stroke: #808080;
    stroke-width: 2;
  }

  .pie.hover-over {
    fill: $primary;
    stroke: $primary;
  }

</style>
