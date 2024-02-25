import React from "react";
import { Helmet } from "react-helmet";

const MetaData = ({ titulo }) => {
  return (
    <Helmet>
      <title>{`${titulo} - Ecommerce Amazon`}</title>
    </Helmet>
  );
};

export default MetaData;
