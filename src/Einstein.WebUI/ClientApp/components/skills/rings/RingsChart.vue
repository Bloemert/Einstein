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
  import { mapRingsActions, mapRingsFields, mapRingsMultiRowFields } from './store';

  const d3 = require('d3');

  export default {
    data: function () {
      return {
        step: 25,
        circleSize: 100,

        svg: {},
        mainGraph: {},
        zoom: {},
        slider: {}
      }
    },

    watch: {
      selectedSeqno: {
        handler: function (newVal, oldVal) {
          // unhighlight old
          d3.selectAll('.highlight').classed('highlight', false);

          // highlight new
          if (!d3.selectAll('circle.seqno-' + newVal).empty()) {
            d3.selectAll('circle.seqno-' + newVal)
              .classed('highlight', true);
          }
        },
        deep: true
      },

      rings: {
        handler: function (newVal, oldVal) {
          this.draw();
        },
        deep: true
      }
    },

    methods: {
      ...mapRingsActions({
        loadData: 'loadFromDB',
      }),

      draw: function () {
            this.step = this.circleSize / this.rings.length;
            var that = this;

            var div = d3.select("body").append("div")
              .attr("class", "tooltip")
              .style("opacity", 0);

            this.svg = d3.select(this.$el).select('svg');
            this.mainGraph = this.svg.append('g')
              .style("transform-origin", "50% 50% 0");

            this.zoom = d3.zoom()
              .scaleExtent([1, 10])
              .on("zoom", function () {
                that.mainGraph
                  .style('transform', 'scale(' + d3.event.transform.k + ')');
                that.slider.property("value", d3.event.scale);
              });

            if (d3.select("div.slider").select('input').empty()) {
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
            }

            this.outerCircle = this.mainGraph
              .append('circle')
              .attr('cx', '250')
              .attr('cy', '150')
              .attr('r', that.circleSize)
              .attr('class', 'outer-ring');

            this.mainGraph.selectAll('circle')
              .data(that.rings)
              .enter()
              .append('circle')
              .attr('cx', '250')
              .attr('cy', '150')
              .attr('r', function (d) { return (d.seqno) * that.step; })
              .attr('class', 'inner-ring')

        this.mainGraph.selectAll('circle.outer-ring').select('circle')
          .data(that.rings)
          .enter()
          .append('circle')
          .attr('cx', '250')
          .attr('cy', '150')
          .attr('r', function (d) {
            return ((d.seqno + 1) * that.step) - (that.step / 2)
          })
          .classed('inner-ring-hover', true)
          .style('stroke-width', (that.step - 2))
          .on("mouseover", function (d) {
            d3.select(this)
              .classed('inner-ring-hover-over', true);

            div.transition()
              .duration(100)
              .style("opacity", .95);
            div.html(d.id + " " + d.name)
              .style("left", (d3.event.pageX) + "px")
              .style("top", (d3.event.pageY - 35) + "px");
          })
          .on("mouseout", function (d) {
            d3.select(this)
              .classed('inner-ring-hover-over', false);

            div.transition()
              .duration(100)
              .style("opacity", 0);
          })
          .on("click", function (d) {
            that.selectedSeqno = d.seqno;
          })
          .each(function (d, i) {
            d3.select(this)
              .classed('seqno-' + d.seqno, true)
          })
              ;

            this.mainGraph.selectAll('circle.outer-ring').select('text')
              .data(that.rings)
              .enter()
              .append("text")
              .classed("inner-ring-text", true)
              .attr("x", 250 * 0.97)
              .attr("y", function (d) { return (150 + ((d.seqno + 1) * that.step)) * 0.98; })
              .text(function (d) { return d.id; });
      }
    },

    computed: {

      ...mapRingsFields({
        rings: 'rows',
        selectedSeqno: 'selectedSeqno',
        currentPage: 'currentPage'
      })

    },

    mounted: function () {
      this.loadData()
        .then((r) => {
          this.draw();
        });
    },



  }
</script>

<style lang="scss">
@import '../../../styles/site.scss';

  .outer-ring {
    fill: none;
    stroke: #000;
    stroke-width: 4;
    transform-origin: 50% 50% 0;
  }

  .inner-ring {
    fill: none;
    stroke: #808080;
    stroke-width: 2;
  }

  .inner-ring-hover {
    fill: none;
    stroke: white;
  }

  .inner-ring-hover-over {
    fill: none;
    stroke: lightgrey;
  }

  .inner-ring-hover.highlight {
    fill: none;
    stroke: $primary;
  }

  .inner-ring-text {
    font-size: larger;
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
</style>
